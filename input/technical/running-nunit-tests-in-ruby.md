Title: "Running NUnit Tests in Ruby"
Published: 29 Jul 2006
Tags: [NUnit V2,OSCON,Ruby]
---
People want what they want. If NUnit doesn't have a feature that they need, they ask for it. Of course, we can't implement all of them, so some people want to be able to create there own test runner using the NUnit assemblies.

<!--more-->
This isn't too hard to do, but it takes a bit of effort to glue the various NUnit classes together. After I attended a talk by John Lam talk on RubyCLR, I got the idea of creating a "clone" of NUnit-console in Ruby.

Initially, I couldn't even get RubyCLR to run on my system, but that was due to my lack of Ruby knowledge as much as anything else. In the end, it worked. The code in this article was written pairing with John, who is probably responsible for it's working at all.

Just to be clear, RubyCLR is **not** an implementation of Ruby for the .Net framework. Rather, it extends standard Ruby so that it can work with the .Net types. Originally, I was hoping to find a full implementation that generated IL, but this approach works now and works pretty well.

To get started with my spike, I needed to make my Ruby code aware of two NUnit assemblies. Here's the code...

```ruby
require "rubyclr"
reference_file 'C:\\Program Files\\NUnit 2.2.8\\bin\\nunit.core.dll'
reference_file 'C:\\Program Files\\NUnit 2.2.8\\bin\\nunit.util.dll'
include NUnit::Core
```

The require statement pulls the RubyCLR extensions. The two reference_file statements make the assemblies I want to use available in Ruby. The include statement allows me to use the types in the NUnit.Core namespace without fully qualifying them. I could have done the same for NUnit.Util, but I left it out for variety.

My next step is to create a SimpleTestRunner object and tell it to load a test assembly. I used some of NUnit's own tests for this purpose. Just to have something to output, I counted the test cases in the NUnit.Framework.Tests namespace.

```ruby
runner = SimpleTestRunner.new
runner.Load 'C:\\Program Files\\NUnit 2.2.8\\bin\\nunit.framework.tests.dll'
count = runner.count_test_cases( "NUnit.Framework.Tests" )
puts "Loaded #{count} test cases"
```

An interesting feature of this code is the use of the count_test_cases method on the TestRunner. Actually, there is no such method! The real method is called CountTestCases. However, RubyCLR allows use of ruby-friendly naming conventions and attempts to match up any methods it can't find by removing underscores and capitalizing as needed. Personally, I think I prefer using the actual names to make it clear that I'm calling into the .Net framework.

Running the tests is pretty simple...

```ruby
result = runner.Run( NullListener.NULL.as( EventListener) )
```

The Run method expects to be passed an EventListener interface. Although Ruby itself doesn't have interfaces, the as method makes this conversion for us. We use NUnit's built-in NullListener for now.

Of course, if we run tests, we would like to know the results. We can do several things here. First, lets create a little summary report, using NUnit's ResultSummarizer.

```ruby
summary = NUnit::Util::ResultSummarizer.new( result )
puts( "#{summary.ResultCount} tests run, #{summary.Failures} failures, #{summary.TestsNotRun} not run" )
```

When I run this, I see that there are two failures. For the moment, we won't worry about these.

It's also pretty easy to write the results to an XML file. We do this with the XmlResultVisitor class, which implements the visitor pattern on a tree of test resuilts.

```ruby
visitor = NUnit::Util::XmlResultVisitor.new( "testresult.xml", result )
result.Accept( visitor )
visitor.Write
```

This creates a file named testresult.xml in your current directory. You could, of course, scan through the XML visually to find the failures, but lets write some Ruby to print the errors from the result tree instead. Here's a quick hack to do just that...

```ruby
class ErrorReporter

  def initialize
    @err_count = 0
  end

  def report_failures( result )
    if result.Test.IsTestCase
      if result.IsFailure
        @err_count = @err_count + 1
        puts "#{@err_count}) #{result.Message}"
      end
      return
    end
    
    result.Results.each { | r | report_failures( r ) }  
  end
 
end

ErrorReporter.new.report_failures( result )
```

Examining the messages from the two failures, we see that one of them is caused by failure to find a config file and the other by failure to find one of the NUnit assemblies. Because we used a SimpleTestRunner, no application domain has been created. So we are using Ruby's own config and ApplicationBase. These are not surprising errors and could be fixed by using NUnit's TestDomain runner rather than SimpleTestRunner. Unfortunately, that causes a security error under RubyCLR, so we'll stick with what we have for now.

Another approach would be to report errors dynamically, as the tests are run, using a Ruby implementation of EventListener. For that, I'll need to learn a bit more.

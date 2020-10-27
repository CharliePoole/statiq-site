const string HOST_SITE = "charliepoole.org";
const string USER_NAME = "charl422";
const string PASSWORD = "pCYBKjKeKn";

const string LOCAL_PATH = "output/";
const string LOCAL_FILES = LOCAL_PATH + "**/*";

const string REMOTE_PATH = "public_html/";
const string HASH_FILE = "hashes.xml";

const string HASH_NOT_FOUND =
    "Error: " + HASH_FILE + " was not found on " + HOST_SITE + "." +
    " To initialize the file, first synchronize the contents of the local directory with" +
    " the site manually and then run \"--target=CreateHash\" or \"--target=Deploy --force\"";
const string HASH_ALREADY_EXISTS =
    "Error: " + HASH_FILE + " already exists on " + HOST_SITE + "." +
    "Use option \"--force\" if you really want to overwrite it.";
const string HASH_FILE_CREATED =
    "Created new hash file on " + HOST_SITE + ".";
const string HASH_FILE_UPDATED =
    "Updated hash file on " + HOST_SITE + ".";

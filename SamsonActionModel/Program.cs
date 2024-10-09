using SamsonActionModel.SamsonWake;

var wakePreprocess = new SamsonWakePreprocess();
//wakePreprocess.Preprocess();
wakePreprocess.PreprocessOne(@"C:\\Users\\lssmith\\Documents\\pdrepos\\Samson\\SamsonClient\\SamsonActionModel\\Data\\SamsonWakeData\\TestData\\Audio\\",
                             @"C:\Users\lssmith\Documents\pdrepos\Samson\SamsonClient\SamsonActionModel\Data\SamsonWakeData\TestData\Spectogram\");
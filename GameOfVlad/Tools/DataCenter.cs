namespace GameOfVlad.Tools
{
    public class DataCenter
    {
        public bool Level1 { get; set; }
        public bool Level2 { get; set; }
        public bool Level3 { get; set; }
        public bool Level4 { get; set; }
        public bool Level5 { get; set; }
        public bool Level6 { get; set; }
        public bool Level7 { get; set; }
        public bool Level8 { get; set; }
        public bool Level9 { get; set; }
        public bool Level10 { get; set; }
        public bool SpecialLevel1 { get; set; }
        public bool Level11 { get; set; }
        public bool Level12 { get; set; }
        public bool Level13 { get; set; }
        public bool Level14 { get; set; }
        public bool SpecialLevel2 { get; set; }
        public bool Level15 { get; set; }
        public bool Level16 { get; set; }
        public bool Level17 { get; set; }
        public bool Level18 { get; set; }
        public bool Level19 { get; set; }
        public bool Level20 { get; set; }
        public bool Level21 { get; set; }
        public bool Level22 { get; set; }
        public bool SpecialLevel3 { get; set; }
        public bool Level23 { get; set; }
        public bool Level24 { get; set; }
        public bool Level25 { get; set; }
        public bool Level26 { get; set; }
        public bool Level27 { get; set; }
        public bool Level28 { get; set; }
        public bool Level29 { get; set; }
        public bool Level30 { get; set; }
        public bool Level31 { get; set; }

        public void LoadData(GameOfVlad game)
        {
            Level1 = game.DataManager.LevelPassed("Level1");
            Level2 = game.DataManager.LevelPassed("Level2");
            Level3 = game.DataManager.LevelPassed("Level3");
            Level4 = game.DataManager.LevelPassed("Level4");
            Level5 = game.DataManager.LevelPassed("Level5");
            Level6 = game.DataManager.LevelPassed("Level6");
            Level7 = game.DataManager.LevelPassed("Level7");
            Level8 = game.DataManager.LevelPassed("Level8");
            SpecialLevel1 = game.DataManager.LevelPassed("Level9");
            Level9 = game.DataManager.LevelPassed("Level10");
            Level10 = game.DataManager.LevelPassed("Level11");
            Level11 = game.DataManager.LevelPassed("Level12");
            Level12 = game.DataManager.LevelPassed("Level13");
            Level13 = game.DataManager.LevelPassed("Level14");
            Level14 = game.DataManager.LevelPassed("Level15");
            SpecialLevel2 = game.DataManager.LevelPassed("Level16");
            Level15 = game.DataManager.LevelPassed("Level17");
            Level16 = game.DataManager.LevelPassed("Level18");
            Level17 = game.DataManager.LevelPassed("Level19");
            Level18 = game.DataManager.LevelPassed("Level20");
            Level19 = game.DataManager.LevelPassed("Level21");
            Level20 = game.DataManager.LevelPassed("Level22");
            Level21 = game.DataManager.LevelPassed("Level23");
            Level22 = game.DataManager.LevelPassed("Level24");
            SpecialLevel3 = game.DataManager.LevelPassed("Level25");
            Level23 = game.DataManager.LevelPassed("Level26");
            Level24 = game.DataManager.LevelPassed("Level27");
            Level25 = game.DataManager.LevelPassed("Level28");
            Level26 = game.DataManager.LevelPassed("Level29");
            Level27 = game.DataManager.LevelPassed("Level30");
            Level28 = game.DataManager.LevelPassed("Level31");
            Level29 = game.DataManager.LevelPassed("Level32");
            Level30 = game.DataManager.LevelPassed("Level33");
            Level31 = game.DataManager.LevelPassed("Level34");
        }
    }
}

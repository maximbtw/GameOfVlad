using System;

namespace GameOfVlad.UI.Forms.GamePause;

public class GamePauseFormEventConfiguration
{
    public Action OnBackToGameBtnClick { get; set; }
    
    public Action OnRestartBtnClick { get; set; }
    
    public Action OnToMapBtnClick { get; set; }
    
    public Action OnToSettingsBtnClick { get; set; }
    
    public Action OnToMainMenuBtnClick { get; set; }
}
function closeWin() {
    GetRadWindow().BrowserWindow.location.href = 'Iniciar.aspx';
    GetRadWindow().close();
}
function GetRadWindow() {
    var oWindow = null; if (window.radWindow)
        oWindow = window.radWindow; else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow; return oWindow;
}
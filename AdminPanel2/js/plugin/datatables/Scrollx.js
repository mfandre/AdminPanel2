$.fn.dataTableExt.oApi.UpdateScrolX = function (oSettings, width) {
    //var oSettings = _fnSettingsFromNode(this[_oExt.iApiIndex]);
    if (width != "") {
        oSettings.oScroll.sXInner = width;
    }
    this.fnDraw();
}
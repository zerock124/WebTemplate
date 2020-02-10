define(["require", "exports", "../Share/PublicFunction"], function (require, exports, PublicFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var FontHomeService = (function () {
        function FontHomeService() {
        }
        FontHomeService.prototype.CreateFontHome = function (model) {
            var setting = {
                url: '/FontHome/CreateFontHome',
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting, 'FormData');
        };
        FontHomeService.prototype.GetFontHomeList = function (SearchModel) {
            var setting = {
                url: '/FontHome/GetFontHomeList',
                type: 'POST',
                data: SearchModel,
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        FontHomeService.prototype.GetEditFontHome = function (FontHomeId) {
            var setting = {
                url: "/FontHome/GetEditFontHome?FontHomeId=" + FontHomeId,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        FontHomeService.prototype.GetEditFontHomeItem = function (FontHomeId) {
            var setting = {
                url: "/FontHome/GetEditFontHomeItem?FontHomeId=" + FontHomeId,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        FontHomeService.prototype.EditFontHomeItem = function (model) {
            var setting = {
                url: "/FontHome/EditFontHomeItem",
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting, "FormData");
        };
        return FontHomeService;
    }());
    var fonthome_service = new FontHomeService();
    exports.default = fonthome_service;
});
//# sourceMappingURL=service.js.map
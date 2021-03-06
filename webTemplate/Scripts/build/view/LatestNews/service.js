var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
define(["require", "exports", "../Share/PublicFunction"], function (require, exports, PublicFunction_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var LatestNewsService = (function () {
        function LatestNewsService() {
        }
        LatestNewsService.prototype.GetLatestNewsList = function (SearchModel, sendPagination) {
            var setting = {
                url: '/LatestNews/GetLatestNewsList',
                type: 'GET',
                data: __assign(__assign({}, SearchModel), sendPagination)
            };
            console.log(setting.data);
            return PublicFunction_1.AjaxReturn(setting);
        };
        LatestNewsService.prototype.CreateLatestNews = function (model) {
            var setting = {
                url: "/LatestNews/CreateLatestNews",
                type: 'POST',
                data: model
            };
            console.log(setting.data);
            return PublicFunction_1.AjaxReturn(setting, "FormData");
        };
        LatestNewsService.prototype.GetEditLatestNewsItem = function (LatestNewsId) {
            var setting = {
                url: "/LatestNews/GetEditLatestNewsItem?LatestNewsId=" + LatestNewsId,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        LatestNewsService.prototype.EditLatestNewsItem = function (model) {
            var setting = {
                url: "/LatestNews/EditLatestNewsItem",
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting, "FormData");
        };
        LatestNewsService.prototype.DeleteLatestNews = function (LatestNewsId) {
            var setting = {
                url: "/LatestNews/DeleteLatestNews?LatestNewsId=" + LatestNewsId,
                type: 'POST',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return LatestNewsService;
    }());
    var fonthome_service = new LatestNewsService();
    exports.default = fonthome_service;
});
//# sourceMappingURL=service.js.map
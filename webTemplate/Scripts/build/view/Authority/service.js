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
    var AuthorityService = (function () {
        function AuthorityService() {
        }
        AuthorityService.prototype.GetAuthorityList = function (SearchModel, sendPagination) {
            var setting = {
                url: '/Authority/GetAuthorityList',
                type: 'POST',
                data: __assign(__assign({}, SearchModel), sendPagination)
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        AuthorityService.prototype.CheckAuthority = function (Id) {
            var setting = {
                url: "/Authority/CheckAuthority?Id=" + Id,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        AuthorityService.prototype.GetEditAuthorityItem = function (Id) {
            var setting = {
                url: "/Authority/GetEditAuthorityItem?Id=" + Id,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        AuthorityService.prototype.EditAuthorityItem = function (model) {
            var setting = {
                url: "/Authority/EditAuthorityItem",
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        AuthorityService.prototype.DeleteAuthorityItem = function (Id) {
            var setting = {
                url: "/Authority/DeleteAuthorityItem",
                type: 'POST',
                data: { Id: Id }
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        AuthorityService.prototype.GetRoleOptions = function () {
            var setting = {
                url: 'Authority/GetRoleOptions',
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return AuthorityService;
    }());
    var authority_service = new AuthorityService();
    exports.default = authority_service;
});
//# sourceMappingURL=service.js.map
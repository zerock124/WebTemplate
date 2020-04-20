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
    var ContactService = (function () {
        function ContactService() {
        }
        ContactService.prototype.GetContactList = function (SearchModel, sendPagination) {
            var setting = {
                url: '/Contact/GetContactList',
                type: 'POST',
                data: __assign(__assign({}, SearchModel), sendPagination)
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        ContactService.prototype.GetEditContactItem = function (ContactId) {
            var setting = {
                url: "/Contact/GetEditContactItem?ContactId=" + ContactId,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        ContactService.prototype.EditContactItem = function (model) {
            var setting = {
                url: "/Contact/EditContactItem",
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        ContactService.prototype.DeleteContact = function (ContactId) {
            var setting = {
                url: "/Contact/DeleteContact",
                type: 'POST',
                data: { ContactId: ContactId }
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return ContactService;
    }());
    var contact_service = new ContactService();
    exports.default = contact_service;
});
//# sourceMappingURL=service.js.map
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
    var CaseService = (function () {
        function CaseService() {
        }
        CaseService.prototype.GetCaseList = function (SearchModel, sendPagination) {
            var setting = {
                url: '/Case/GetCaseList',
                type: 'POST',
                data: __assign(__assign({}, SearchModel), sendPagination)
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        CaseService.prototype.GetEditCaseItem = function (CaseId) {
            var setting = {
                url: "/Case/GetEditCaseItem?CaseId=" + CaseId,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        CaseService.prototype.EditCaseItem = function (model) {
            var setting = {
                url: "/Case/EditCaseItem",
                type: 'POST',
                data: model
            };
            return PublicFunction_1.AjaxReturn(setting, "FormData");
        };
        CaseService.prototype.DeleteCaseItem = function (CaseId) {
            var setting = {
                url: "/Case/DeleteCaseItem?CaseId=" + CaseId,
                type: 'GET',
            };
            return PublicFunction_1.AjaxReturn(setting);
        };
        return CaseService;
    }());
    var case_service = new CaseService();
    exports.default = case_service;
});
//# sourceMappingURL=service.js.map
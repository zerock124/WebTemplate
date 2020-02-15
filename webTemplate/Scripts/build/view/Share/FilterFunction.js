define(["require", "exports", "moment"], function (require, exports, moment) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    moment.locale('zh-tw');
    exports.dateToDateTimeString = function (date, emptystr) {
        if (emptystr === void 0) { emptystr = '-'; }
        if (date)
            return moment(date).format('YYYY-MM-DD HH:mm:ss');
        return emptystr;
    };
    exports.dateToDateString = function (date, emptystr) {
        if (emptystr === void 0) { emptystr = '-'; }
        if (date)
            return moment(date).format('YYYY-MM-DD');
        return emptystr;
    };
    exports.dateToTimeString = function (date, emptystr) {
        if (emptystr === void 0) { emptystr = '-'; }
        if (date)
            return moment(date).format('HH:mm:ss');
        return emptystr;
    };
    exports.GetLatestNewsEnum = function (date) {
        switch (date) {
            case 0:
                return '媒體報導';
            case 1:
                return '新聞報導';
            case 2:
                return '測試報導';
            default:
                return '-';
        }
    };
    exports.GetCaseEnum = function (date) {
        switch (date) {
            case 0:
                return '行銷活動';
            case 1:
                return '臉書機器人';
            case 2:
                return '官網';
            default:
                return '-';
        }
    };
});
//# sourceMappingURL=FilterFunction.js.map
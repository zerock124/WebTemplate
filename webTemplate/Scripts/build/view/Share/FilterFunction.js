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
    exports.ValueOrHyphen = function (val) {
        if (val === null || val === undefined || val === "")
            return "-";
        return val;
    };
    exports.CurrencyFormat = function (num) {
        if (typeof (num) !== 'number') {
            return num;
        }
        var _num = num.toString().split('.');
        _num[0] = _num[0].replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        return _num.join('.');
    };
    exports.MeterToKm = function (meter) {
        return (meter / 1000).toFixed(1);
    };
});
//# sourceMappingURL=FilterFunction.js.map
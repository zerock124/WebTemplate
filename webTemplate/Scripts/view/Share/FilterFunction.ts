import moment = require("moment");

moment.locale('zh-tw');
export const dateToDateTimeString = (date: Date | null, emptystr: any = '-') => {
    if (date)
        return moment(date).format('YYYY-MM-DD HH:mm:ss');
    return emptystr;
}

export const dateToDateString = (date: Date | null, emptystr: any = '-') => {
    if (date)
        return moment(date).format('YYYY-MM-DD');
    return emptystr;
}

export const dateToTimeString = (date: Date | null, emptystr: any = '-') => {
    if (date)
        return moment(date).format('HH:mm:ss');
    return emptystr;
}

export const GetLatestNewsEnum = (date: number | null) => {
    switch (date) {
        case 0:
            return '媒體報導';
        case 1:
            return '新聞報導';
        case 2:
            return '測試報導';
        default:
            return '-'
    }
}

export const GetCaseEnum = (date: number | null) => {
    switch (date) {
        case 0:
            return '行銷活動';
        case 1:
            return '臉書機器人';
        case 2:
            return '官網';
        default:
            return '-'
    }
}
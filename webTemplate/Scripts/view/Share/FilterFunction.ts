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

/**
 * 內容為空白時傳回( - )
 * @param val
 */
export const ValueOrHyphen = (val: string | number | undefined) => {
	if (val === null || val === undefined || val === "")
		return "-";
	return val;
}

/**
 * 千分位 (目前只限數字型態)
 * @param num
 */
export const CurrencyFormat = (num: number | string): string => {
	if (typeof (num) !== 'number') { return num; }
	const _num = num.toString().split('.');
	_num[0] = _num[0].replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
	return _num.join('.');
}

/**
 * 公尺轉公里
 * @param meter 公尺
 */
export const MeterToKm = (meter: number) => {
	return (meter / 1000).toFixed(1);
}

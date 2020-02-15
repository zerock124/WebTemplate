export interface IHomeService {
    /**
     * 取得HomeDate
     * @param SearchModel
     */
    GetHomeDate(): JQuery.jqXHR<ResWithPaginationViewModel<HomeViewModel>>;
}

export interface HomeViewModel {
    HomeDataNumber: number;
    CaseDataNumber: number;
}
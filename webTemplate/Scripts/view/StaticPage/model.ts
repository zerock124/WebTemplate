export interface IStaticPageService {

    /**取得靜態頁內容 */
    GetStaticPageList(): JQuery.jqXHR<ResponseViewModel<StaticPageViewModel[]>>
    /**
     * 新增靜態頁內容
     * @param model
     */
    CreateStaticPage(model: StaticPageViewModel): JQuery.jqXHR<ResponseViewModel>
    /**
     * 變更靜態頁內容
     * @param model
     */
    EditStaticPage(model: StaticPageViewModel): JQuery.jqXHR<ResponseViewModel>
}

export interface StaticPageViewModel {
    /**靜態頁ID */
    StaticPageId: number;
    /**靜態頁類別 */
    StaticPageEnum: number;
    /**靜態頁內容 */
    PageContent: string;
    /**新建時間 */
    CreateTime: Date;
    /**新增的使用者 */
    CreateUser: string;
    /**更新時間 */
    UpdateTime: Date;
    /**更新的使用者 */
    UpdateUser: string;
}


export interface ILatestNewsService {
    /**
     * 取得最新消息列表
     * @param SearchModel
     */
    GetLatestNewsList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<LatestNewsViewModel[]>>
    /**
     * 新增最新消息
     * @param model
     */
    CreateLatestNews(model: LatestNewsViewModel): JQuery.jqXHR<ResponseViewModel>;
    /**
     * 取得變更最新消息圖片
     * @param LatestNewsId
     */
    GetEditLatestNewsItem(LatestNewsId: number): JQuery.jqXHR<ResponseViewModel>
    /**
     * 變更最新消息
     * @param model
     */
    EditLatestNewsItem(model: LatestNewsViewModel): JQuery.jqXHR<ResponseViewModel>
    /**
     * 刪除最新消息
     * @param LatestNewsId
     */
    DeleteLatestNews(LatestNewsId: number): JQuery.jqXHR<ResponseViewModel>
}

export interface LatestNewsViewModel {
    /**最新消息ID */
    LatestNewsId: number;
    /**最新消息圖片檔名 */
    ImageName: string;
    /**最新消息類別 */
    LatestNewsEnum: number;
    /**上線時間 */
    StartDateTime: Date;
    /**最新消息標題 */
    LatestNewsTitle: string;
    /**最新消息內容 */
    LatestNewsContent: string;
    /**是否開啟 */
    Status: boolean;
    /**備註 */
    Remark: string;
    /**新建時間 */
    CreateTime: Date;
    /**更新時間 */
    UpdateTime: Date | null;
    /**首頁圖片檔案 */
    PhotoFile: File | null;
}

export interface SearchModel {
    SearchEnum: number;
    StartDateTime: Date | null;
    EndDateTime: Date | null;
    StartOnlineDateTime: Date | null;
    EndOnlineDateTime: Date | null;
    Query: string;
}
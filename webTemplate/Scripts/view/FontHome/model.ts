export interface IFontHomeService {
    /**
     * 新增前台首頁圖片
     * @param model
     */
    CreateFontHome(model: FontHomeViewModel): JQuery.jqXHR<ResponseViewModel>
    /**
     * 取得前台首頁圖片列表
     * @param SearchModel
     */
    GetFontHomeList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<FontHomeViewModel[]>>
    /**
     * 變更前台首頁圖片
     * @param model
     */
    GetEditFontHome(FontHomeId: number): JQuery.jqXHR<ResponseViewModel>
    /**
     * 取得變更前台首頁圖片
     * @param FontHomeId
     */
    GetEditFontHomeItem(FontHomeId: number): JQuery.jqXHR<ResponseViewModel>
    /**
     * 邊更前台首頁Item
     * @param model
     */
    EditFontHomeItem(model: FontHomeViewModel): JQuery.jqXHR<ResponseViewModel> 
    /**
     * 刪除前台首頁圖片
     * @param FontHomeId
     */
    DeleteFontHome(FontHomeId: number): JQuery.jqXHR<ResponseViewModel>
}

export interface FontHomeViewModel {
    /**首頁圖片Id */
    FontHomeId: number;
    /**首頁圖片檔名 */
    ImageName: string;
    /**首頁網址 */
    FontHomeUrl: string;
    /**上線時間 */
    StartDateTime: Date;
    /**下線時間 */
    EndDateTime: Date;
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
    /**圖片URL */
    ImgUrl: string;
    /**確認是否為上限 */
    CheckOnlineDate: boolean;
}

export interface SearchModel {
    SearchEnum: number;
    StartDateTime: Date | null;
    EndDateTime: Date | null;
    OnlineDateTime: Date | null;
    Query: string;
}
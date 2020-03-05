export interface ICaseService {
    /**
     * 取得案例介紹列表
     * @param SearchModel
     */
    GetCaseList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<CaseViewModel[]>>;
    /**
     * 新增案例介紹
     * @param model
     */
    CreateCaseItem(model: CaseViewModel): JQuery.jqXHR<ResponseViewModel>;
    /**
     * 取得變更案例介紹圖片
     * @param LatestNewsId
     */
    GetEditCaseItem(CaseId: number): JQuery.jqXHR<ResponseViewModel>;
    /**
     * 變更案例介紹
     * @param model
     */
    EditCaseItem(model: CaseViewModel): JQuery.jqXHR<ResponseViewModel>;
    /**
     * 刪案例介紹
     * @param CaseId
     */
    DeleteCaseItem(CaseId: number): JQuery.jqXHR<ResponseViewModel>;
}

export interface CaseViewModel {
    /**案例ID */
    CaseId: number;
    /**案例圖片名稱 */
    ImageName: string;
    /**案例連接網址 */
    CaseUrl: number;
    /**案例類別 */
    CaseEnum: Date;
    /**案例名稱 */
    CaseName: string;
    /**案例內容 */
    CaseContent: string;
    /**是否開啟 */
    Status: boolean;
    /**新建時間 */
    CreateTime: Date;
    /**新建的使用者 */
    CreateUser: string;
    /**更新時間 */
    UpdateTime: Date | null;
    /**更新的使用者 */
    UpdateUser: string;
    /**首頁圖片檔案 */
    PhotoFile: File | null;
    LabelTag: string[];
}

export interface SearchModel {
    SearchEnum: number;
    StartDateTime: Date | null;
    EndDateTime: Date | null;
    Query: string;
}
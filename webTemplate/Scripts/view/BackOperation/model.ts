export interface IBackOperationService {
    /**
     * 取得後台操作紀錄列表
     * @param SearchModel
     */
    GetBackOperationList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<BackOperationViewModel[]>>
}

export interface BackOperationViewModel {
    /**操作紀錄Id */
    BackOperationId: number;
    /**使用者ID */
    AspNetUserId: string;
    /**使用者帳號 */
    UserName: string;
    /**角色ID */
    RoleId: string;
    /**角色名稱 */
    RoleName: string;
    /**操作紀錄內容 */
    ContentText: Date;
    /**結果 */
    Result: boolean;
    /**IP */
    IP: string;
    /**新建時間 */
    CreateTime: Date;
}

export interface SearchModel {
    SearchEnum: number;
    StartDateTime: Date | null;
    EndDateTime: Date | null;
    Query: string;
}
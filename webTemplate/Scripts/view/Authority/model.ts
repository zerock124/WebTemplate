export interface IAuthorityService {
    /**
     * 取得最新消息列表
     * @param SearchModel
     */
    GetAuthorityList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<AuthorityViewModel[]>>
    /**
     * 確認是否有權限
     * @param CaseId
     */
    CheckAuthority(Id: string): JQuery.jqXHR<ResponseViewModel>
    /**
     * 取得變更最新消息圖片
     * @param LatestNewsId
     */
    GetEditAuthorityItem(Id: string): JQuery.jqXHR<ResponseViewModel>
    /**
     * 變更最新消息
     * @param model
     */
    EditAuthorityItem(model: AuthorityViewModel): JQuery.jqXHR<ResponseViewModel>
    /**
     * 刪除最新消息
     * @param LatestNewsId
     */
    DeleteAuthorityItem(Id: string): JQuery.jqXHR<ResponseViewModel>

    GetRoleOptions(): JQuery.jqXHR<ResponseViewModel>
}

export interface AuthorityViewModel {
    /**帳號ID */
    Id: string;
    /**真實姓名 */
    RealName: string;
    /**帳號 */
    UserName: string;
    /**密碼 */
    Password: string;
    /**角色ID */
    RoleId: string;
    /**新建的使用者ID */
    CreateUser: string;
    /**新建的使用者帳號 */
    CreateName: string;
    /**新建時間 */
    CreateTime: Date;
    /**更新時間 */
    UpdateTime: Date | null;
}

export interface SearchModel {
    SearchEnum: number;
    StartDateTime: Date | null;
    EndDateTime: Date | null;
    Query: string;
}
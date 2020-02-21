export interface IContactService {
    /**
     * 取得前台首頁圖片列表
     * @param SearchModel
     */
    GetContactList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<ContactViewModel[]>>
    /**
     * 取得變更前台首頁圖片
     * @param FontHomeId
     */
    GetEditContactItem(ContactId: number): JQuery.jqXHR<ResponseViewModel>
    /**
     * 邊更前台首頁Item
     * @param model
     */
    EditContactItem(model: ContactViewModel): JQuery.jqXHR<ResponseViewModel> 
    /**
     * 刪除前台首頁圖片
     * @param FontHomeId
     */
    DeleteContact(ContactId: number): JQuery.jqXHR<ResponseViewModel>
}

export interface ContactViewModel {
    /**聯絡我們Id */
    ContactId: number;
    /**公司名稱 */
    CompanyName: string;
    /**聯絡人姓名 */
    Name: string;
    /**性別 */
    Sex: number;
    /**Email */
    Email: string;
    /**連絡電話 */
    ContactPhone: string;
    /**類型 */
    Status: string;
    /**預算 */
    Budget: number;
    /**預計上線日期 */
    OnlineDate: Date | null;
    /**需求說明 */
    Demand: string;
    /**備註 */
    Remark: string;
    /**建立日期 */
    CreateTime: Date;
    /**更新日期 */
    UpdateTime: Date;
    /**更新的使用者 */
    UpdateUser: string;
}

export interface SearchModel {
    SearchEnum: number;
    StartDateTime: Date | null;
    EndDateTime: Date | null;
    Query: string;
}
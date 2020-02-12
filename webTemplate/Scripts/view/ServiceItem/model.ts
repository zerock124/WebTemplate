export interface IServiceItemService {

    /**取得靜態頁內容 */
    GetServiceItemList(): JQuery.jqXHR<ResponseViewModel<ServiceItemViewModel[]>>
}

export interface ServiceItemViewModel {
    /**服務項目ID */
    ServiceItemId: number;
    /**服務項目名稱 */
    ServiceItemName: string;
    /**圖片名稱 */
    ImageName: string;
    /**新建時間 */
    CreateTime: Date;
    /**新增的使用者 */
    CreateUser: string;
    /**更新時間 */
    UpdateTime: Date;
    /**更新的使用者 */
    UpdateUser: string;
}

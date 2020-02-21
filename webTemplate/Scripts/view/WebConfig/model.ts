export interface IWebConfigService {
    /**
     * 取得網站設定內容
     * @param model
     */
    GetWebConfig(Id: number): JQuery.jqXHR<ResponseViewModel>

    /**
     * 邊更網站設定內容
     * @param model
     */
    EditWebConfig(model: WebConfigViewModel): JQuery.jqXHR<ResponseViewModel>
}

export interface WebConfigViewModel {
    /**網站設定Id */
    Id: number;
    /**網站名稱 */
    ImageName: string;
    /**客服信箱 */
    ServiceMail: string;
    /**Copyright */
    Copyright: string;
    /**系統寄件者名稱 */
    SystemEmailName: string;
    /**系統寄件者信箱 */
    SystemEmail: string;
    /**meta_title */
    meta_title: string;
    /**meta_keyword */
    meta_keyword: string;
    /**meta_description */
    meta_description: string;
    /**meta_url */
    meta_url: string;
    /**meta_image */
    meta_image: string;
}
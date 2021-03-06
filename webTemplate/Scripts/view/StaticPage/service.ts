﻿import { IStaticPageService, StaticPageViewModel, } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class StaticPageService implements IStaticPageService {

    GetStaticPageList(): JQuery.jqXHR<ResponseViewModel<StaticPageViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/StaticPage/GetStaticPageList',
            type: 'GET'
        }

        return AjaxReturn(setting);
    }

    EditStaticPage(model: StaticPageViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: '/StaticPage/EditStaticPage',
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting);
    }

}

const static_service: IStaticPageService = new StaticPageService();
export default static_service;
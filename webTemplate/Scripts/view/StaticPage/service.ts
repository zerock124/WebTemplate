import { IStaticPageService, StaticPageViewModel, } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class StaticPageService implements IStaticPageService {

    GetStaticPageList(): JQuery.jqXHR<ResponseViewModel<StaticPageViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/StaticPage/GetStaticPageList',
            type: 'GET'
        }

        return AjaxReturn(setting);
    }

    CreateStaticPage(model: StaticPageViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: '/StaticPage/CreateStaticPage',
            type: 'POST',
            data: model
        };

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

const fonthome_service: IStaticPageService = new StaticPageService();
export default fonthome_service;
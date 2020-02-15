import { IFontHomeService, FontHomeViewModel, SearchModel } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class FontHomeService implements IFontHomeService {
    CreateFontHome(model: FontHomeViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: '/FontHome/CreateFontHome',
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting, 'FormData');
    }

    GetFontHomeList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<FontHomeViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/FontHome/GetFontHomeList',
            type: 'POST',
            data: {
                ...SearchModel,
                ...sendPagination
            }
        }
        return AjaxReturn(setting);
    }

    GetEditFontHome(FontHomeId: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/FontHome/GetEditFontHome?FontHomeId=${FontHomeId}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    GetEditFontHomeItem(FontHomeId: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/FontHome/GetEditFontHomeItem?FontHomeId=${FontHomeId}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    EditFontHomeItem(model: FontHomeViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/FontHome/EditFontHomeItem`,
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting, "FormData");
    }

    DeleteFontHome(FontHomeId: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/FontHome/DeleteFontHome`,
            type: 'POST',
            data: { FontHomeId }
        };

        return AjaxReturn(setting);
    }
}

const fonthome_service: IFontHomeService = new FontHomeService();
export default fonthome_service;
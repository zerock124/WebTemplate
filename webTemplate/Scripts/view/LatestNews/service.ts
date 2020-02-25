﻿import { ILatestNewsService, LatestNewsViewModel, SearchModel } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class LatestNewsService implements ILatestNewsService {

    GetLatestNewsList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<LatestNewsViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/LatestNews/GetLatestNewsList',
            type: 'POST',
            data: {
                ...SearchModel,
                ...sendPagination
            }
        }
        return AjaxReturn(setting);
    }

    GetEditLatestNewsItem(LatestNewsId: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/LatestNews/GetEditLatestNewsItem?LatestNewsId=${LatestNewsId}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    EditLatestNewsItem(model: LatestNewsViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/LatestNews/EditLatestNewsItem`,
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting, "FormData");
    }

    DeleteLatestNews(LatestNewsId: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/LatestNews/DeleteLatestNews?LatestNewsId=${LatestNewsId}`,
            type: 'POST',
        }
        return AjaxReturn(setting);
    }

}

const fonthome_service: ILatestNewsService = new LatestNewsService();
export default fonthome_service;
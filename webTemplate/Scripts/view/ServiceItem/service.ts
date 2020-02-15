﻿import { ServiceItemViewModel, IServiceItemService, } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class ServiceItemService implements IServiceItemService {

    GetServiceItemList(): JQuery.jqXHR<ResponseViewModel<ServiceItemViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/ServiceItem/GetServiceItemList',
            type: 'GET'
        }

        return AjaxReturn(setting);
    }

    CreateServiceItemList(model: ServiceItemViewModel[]): JQuery.jqXHR<ResponseViewModel<ServiceItemViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/ServiceItem/CreateServiceItemList',
            type: 'POST',
            data: model
        }

        return AjaxReturn(setting, 'FormData');
    }
}

const fonthome_service: IServiceItemService = new ServiceItemService();
export default fonthome_service;


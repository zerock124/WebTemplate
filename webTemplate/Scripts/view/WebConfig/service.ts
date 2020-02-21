import { WebConfigViewModel, IWebConfigService } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class WebConfigService implements IWebConfigService {
    GetWebConfig(Id: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/WebConfig/GetWebConfig?Id=${Id}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    EditWebConfig(model: WebConfigViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/WebConfig/EditWebConfig`,
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting);
    }
}

const webconfig_service: IWebConfigService = new WebConfigService();
export default webconfig_service;
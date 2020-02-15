import { ICaseService, CaseViewModel, SearchModel } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class CaseService implements ICaseService {

    GetCaseList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<CaseViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/Case/GetCaseList',
            type: 'POST',
            data: {
                ...SearchModel,
                ...sendPagination
            }
        }
        return AjaxReturn(setting);
    }

    GetEditCaseItem(CaseId: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Case/GetEditCaseItem?CaseId=${CaseId}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    EditCaseItem(model: CaseViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Case/EditCaseItem`,
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting, "FormData");
    }

    DeleteCaseItem(CaseId: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Case/DeleteCaseItem?CaseId=${CaseId}`,
            type: 'GET',
        }
        return AjaxReturn(setting);
    }

}

const case_service: ICaseService = new CaseService();
export default case_service;
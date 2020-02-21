import { BackOperationViewModel, IBackOperationService, SearchModel } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class BackOperationService implements IBackOperationService {

    GetBackOperationList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<BackOperationViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/BackOperation/GetBackOperationList',
            type: 'POST',
            data: {
                ...SearchModel,
                ...sendPagination
            }
        }
        return AjaxReturn(setting);
    }
}

const backOperation_service: IBackOperationService = new BackOperationService();
export default backOperation_service;
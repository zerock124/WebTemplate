import { IAuthorityService, AuthorityViewModel, SearchModel } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class AuthorityService implements IAuthorityService {

    GetAuthorityList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<AuthorityViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/Authority/GetAuthorityList',
            type: 'POST',
            data: {
                ...SearchModel,
                ...sendPagination
            }
        }
        return AjaxReturn(setting);
    }

    CheckAuthority(Id: string): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Authority/CheckAuthority?Id=${Id}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    GetEditAuthorityItem(Id: string): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Authority/GetEditAuthorityItem?Id=${Id}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    EditAuthorityItem(model: AuthorityViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Authority/EditAuthorityItem`,
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting);
    }

    DeleteAuthorityItem(Id: string): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Authority/DeleteAuthorityItem`,
            type: 'POST',
            data: { Id }
        }
        return AjaxReturn(setting);
    }

    GetRoleOptions(): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: 'Authority/GetRoleOptions',
            type: 'GET',
        }
        return AjaxReturn(setting);
    }

}

const authority_service: IAuthorityService = new AuthorityService();
export default authority_service;
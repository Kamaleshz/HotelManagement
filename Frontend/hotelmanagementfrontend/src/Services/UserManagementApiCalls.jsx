import axios from "axios";
import UserManagementConstants from "../Utilities/UserModuleConstants";

class UserManagementService {
    http = axios.create({
        baseURL : UserManagementConstants.BaseURL
    })

    async login(obj) {
        let responce = await this.http.post(UserManagementConstants.Login, obj);
        return responce.data;
    }
    
    async register(obj) {
        let responce = await this.http.post(UserManagementConstants.Register, obj);
        return responce.data;
    }

    async update(obj) {
        let responce = await this.http.put(UserManagementConstants.Update, obj);
        return responce.data;
    }
}

export default new UserManagementService();
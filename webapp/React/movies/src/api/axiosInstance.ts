import axios, { AxiosResponse } from "axios";
import { PaginationResult } from "../models/pagination";



const axiosInstance = axios.create({
    baseURL : `${import.meta.env.VITE_APP_WEB_API}/api`    
});

let isInterceptorSetup = false;

export const setupResponseInterceptor = () => {

    if (!isInterceptorSetup) {
        axiosInstance.interceptors.response.use(
            (response: AxiosResponse) => { 
                const paginationParams = response.headers['x-pagination'];
                if (paginationParams)
                {
                    response.data = new PaginationResult(response.data, JSON.parse(paginationParams));
                    return response as AxiosResponse<PaginationResult<any>>;
                }
                return response;
            },
            (error) => {
                 
                if (error.response) {
                    const statusCode = error.response.status;
                    const data = error.response.data;
                    switch (statusCode) {
                        case 400:
                            if (data.errors) {
                                const modalStateError = [];
                                for (const item of data.errors) {
                                    const property = item.property;
                                    const errorMessage = item.errorMessage;

                                    if (property && errorMessage) {
                                        modalStateError.push({ property, errorMessage })
                                    }
                                  
                                }
                                console.log(modalStateError);
                            }
                            break;
                        case 401:
                            console.log("unauthorized");
                            break;
                        case 403:
                            console.log("forbiden");
                            break
                        case 404:
                            console.log("not found");
                            break;
                        default:
                            console.log("general error");
                    }
                }
                return Promise.reject(error);
            }
        )
        isInterceptorSetup = true;
    }
};

setupResponseInterceptor();

export default axiosInstance;

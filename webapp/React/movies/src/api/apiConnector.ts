import axios, { AxiosResponse } from "axios";
import { MovieDto } from "../models/movieDto";
import { GetMovieResponse } from "../models/getMoviesResponse";
import { API_BASE_URL } from "../../config"
import { GetMoviesByIdResponse } from "../models/getMovieByIdResponse";
import { UserDto } from "../models/userDto";
import { token } from "../models/token";
import { getTokenResponse } from "../models/getTokenResponse";
import authSvc from "../auth/authSvc";
import { PaginationRequestParams, PaginationResult } from "../models/pagination";
import axiosInstance from "./axiosInstance";

const apiConnector = {

    getMovies: async (): Promise<MovieDto[]> => {

            authSvc.setAuthHeader();
            const response: AxiosResponse<GetMovieResponse> =
                await axiosInstance.get(`/movies`)
            const movies = response.data.movieDtos.map(movie => ({
                ...movie,
                createDate: movie.createDate?.slice(0, 10) ?? ""
            }));
            return movies;

    },
    getPaginatedMovies: async (paginationRequestParams: PaginationRequestParams): Promise<PaginationResult<MovieDto[]>> => {

            authSvc.setAuthHeader();
            const response: AxiosResponse<PaginationResult<MovieDto[]>> =
                await axiosInstance.get(`/movies/paginated?pageSize=${paginationRequestParams.pageSize}&pageNumber=${paginationRequestParams.pageNumber}`)
       
        if (response.data && Array.isArray(response.data.data))
        {
            const modifiedData = response.data.data.map(movie => ({
                ...movie,
                createDate: movie.createDate?.slice(0, 10) ?? ""

            }));
            return {
                ...response.data, data: modifiedData
            }
            
        } else {
            return {
                data: [],
                paginationParams: {
                    totalItems: 0,
                    totalPages: 0,
                    currentPage: 0,
                    pagesize:0
                }
            }
        }

    },

    createMovie: async (movie: MovieDto): Promise<void> => {
            authSvc.setAuthHeader();
            await axiosInstance.post<number>(`/movies`, movie);
      
    },


    editMovie: async (movie: MovieDto, movieId: number): Promise<void> => {
   
            authSvc.setAuthHeader();
            await axiosInstance.put<number>(`movies/${movieId}`, movie);
    },

    deleteMovie: async (movieId: number): Promise<void> => {
        authSvc.setAuthHeader();
        await axiosInstance.delete<number>(`/movies/${movieId}`);
        
    },

    getMovieById: async (movieId: string): Promise<MovieDto | undefined> => {     
            authSvc.setAuthHeader();
            const response = await axios.get<GetMoviesByIdResponse>(`${API_BASE_URL}/movies/${movieId}`);
            const data = response.data.movieDto;
            return data;

        },
        
    registerUser: async (user: UserDto): Promise<void> => {            
            await axiosInstance.post<number>(`/register`,user);
        },


    loginUser: async (user: UserDto): Promise<token> => {             
        const response = await axiosInstance.post<getTokenResponse>('/login', user);
        return response.data.accessToken;
    }
}

export default apiConnector;
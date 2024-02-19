export interface paginationParams
{
    currentPage: number;
    pagesize: number;
    totalItems: number;
    totalPages: number;

}
export class PaginationRequestParams
{
    pageSize: number;
    pageNumber: number;

    constructor(pageSize:number, pageNumber:number)
    {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
    }
}

export class PaginationResult<T>
{
    data: T;
    paginationParams: paginationParams | undefined;

    constructor(data: T, paginationParams: paginationParams)
    {
        this.data = data;
        this.paginationParams = paginationParams;
    }
}
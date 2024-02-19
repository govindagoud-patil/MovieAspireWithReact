import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { paginationParams, PaginationRequestParams } from '../../models/pagination'
// Define a type for the slice state



// Define the initial state using that type
const initialState = {
    // currentPage: 0,
    // pagesize: 0,
    // totalItems: 0,
    // totalPages: 0,
    paginationParam: {
    currentPage: 0,
    pagesize: 0,
    totalItems: 0,
    totalPages: 0
  } as paginationParams,
  paginationRequestParam: {
    pageSize: 5,
    pageNumber:0
  } as PaginationRequestParams
}



export const paginationSlice = createSlice({
  name: 'pagination',
  initialState,
  reducers: {
    setPgParams: (state, action: PayloadAction<paginationParams>) => {
      state.paginationParam = action.payload
    },
    setPgRequestParams: (state, action: PayloadAction<PaginationRequestParams>) => {
      state.paginationRequestParam = action.payload
    },
  }
})

export const {  setPgParams,setPgRequestParams } = paginationSlice.actions;

export default paginationSlice.reducer;


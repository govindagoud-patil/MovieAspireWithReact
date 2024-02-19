import { configureStore } from '@reduxjs/toolkit';
import counterReducer from './../feature/pagination/couterSlice';
import paginationReducer from './../feature/pagination/paginationSlice';

export const store = configureStore({
  reducer: {
        counter: counterReducer,
        pagination: paginationReducer,
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;


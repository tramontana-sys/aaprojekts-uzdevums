import axios from 'axios';
import { Dispatch } from 'redux';
import { User } from '../types/User';

export const FETCH_USERS = 'FETCH_USERS';
export const CREATE_USER = 'CREATE_USER';
export const UPDATE_USER = 'UPDATE_USER';
export const DELETE_USER = 'DELETE_USER';

const API_URL = process.env.REACT_APP_USERS_API_URL || "undefined";

interface FetchUsersAction {
  type: typeof FETCH_USERS;
  payload: User[];
}

interface CreateUserAction {
  type: typeof CREATE_USER;
  payload: User;
}

interface UpdateUserAction {
  type: typeof UPDATE_USER;
  payload: User;
}

interface DeleteUserAction {
  type: typeof DELETE_USER;
  payload: number;
}

export type UserActionTypes =
  | FetchUsersAction
  | CreateUserAction
  | UpdateUserAction
  | DeleteUserAction;

export const fetchUsers = () => async (dispatch: Dispatch<UserActionTypes>) => {
  const response = await axios.get(API_URL);
  dispatch({ type: FETCH_USERS, payload: response.data });
};

export const createUser = (user: User) => async (dispatch: Dispatch<UserActionTypes>) => {
  const response = await axios.post(API_URL, user);
  dispatch({ type: CREATE_USER, payload: response.data });
};

export const updateUser = (user: User) => async (dispatch: Dispatch<UserActionTypes>) => {
  const response = await axios.put(`${API_URL}/${user.id}`, user);
  dispatch({ type: UPDATE_USER, payload: response.data });
};

export const deleteUser = (id: number) => async (dispatch: Dispatch<UserActionTypes>) => {
  await axios.delete(`${API_URL}/${id}`);
  dispatch({ type: DELETE_USER, payload: id });
};

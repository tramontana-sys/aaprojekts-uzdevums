import { User } from '../types/User';
import {
  FETCH_USERS,
  CREATE_USER,
  UPDATE_USER,
  DELETE_USER,
  UserActionTypes
} from './actions';

interface UserState {
  users: User[];
}

const initialState: UserState = {
  users: []
};

export const userReducer = (state = initialState, action: UserActionTypes): UserState => {
  switch (action.type) {
    case FETCH_USERS:
      return { users: action.payload };
    case CREATE_USER:
      return { users: [...state.users, action.payload] };
    case UPDATE_USER:
      return {
        users: state.users.map(user =>
          user.id === action.payload.id ? action.payload : user
        )
      };
    case DELETE_USER:
      return { users: state.users.filter(user => user.id !== action.payload) };
    default:
      return state;
  }
};
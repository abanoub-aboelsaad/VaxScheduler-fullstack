import { createContext, useReducer, useEffect } from 'react';
import { jwtDecode } from "jwt-decode";
// import PrivateRoute from './PrivateRoute';

export const AuthContext = createContext();

export const authReducer = (state, action) => {
  switch (action.type) {
    case 'LOGIN':
      sessionStorage.setItem('auth', JSON.stringify(action.payload)); 
      return { username: action.payload.userName, token: action.payload.token }; 
    case 'LOGOUT':
      sessionStorage.removeItem('auth'); 
      return { username: null, token: null };
    default:
      return state;
  }
};

export const decodeToken = (token) => {
  if (token) {
    return jwtDecode(token); 
  }
  return null;
};

export const AuthContextProvider = ({ children }) => {
  const [state, dispatch] = useReducer(authReducer, {
    username: null,
    token: null
  });

  useEffect(() => {
    const storedAuth = JSON.parse(sessionStorage.getItem('auth'));

    if (storedAuth) {
      dispatch({ type: 'LOGIN', payload: storedAuth });
    }
  }, []);

  return (
    <AuthContext.Provider value={{ ...state, dispatch, decodeToken }}>
      {/* <PrivateRoute /> */}
      {children}
     
    </AuthContext.Provider>
  );
};














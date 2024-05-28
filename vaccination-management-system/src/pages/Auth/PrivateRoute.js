import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuthContext } from './hooks/useAuthContext';
import { useNavigate } from 'react-router-dom';
const PrivateRoute = ({ element, path, roles }) => {
  const { decodeToken } = useAuthContext();
  const storedAuth = sessionStorage.getItem('auth');
  const authObject = JSON.parse(storedAuth);
  const token = authObject ? authObject.token : null;
  const decodedToken = decodeToken(token);
  const role = decodedToken?.role;
  const isAuthenticated = !!token;
  const isAuthorized = isAuthenticated && roles.includes(role);
  const navigate = useNavigate();



  if (isAuthenticated && (path === '/' ||path === '/login' || path === '/SignUp')) {
    switch (role) {
      case 'Admin':
        navigate('/ManageCenters');
        break;
      case 'VaccinationCenter':
        navigate('/CenterVaccines');
        break;
      default:
        navigate('/ListCenters');
    }
  }


  if (!isAuthenticated && (path === '/' ||path === '/login' || path === '/SignUp')) {
    return element;
  }

  return isAuthorized ? element : <Navigate to="/NotAuthorized" replace />;

};


export default PrivateRoute;

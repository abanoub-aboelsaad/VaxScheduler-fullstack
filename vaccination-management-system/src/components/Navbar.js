import React from 'react';
import { NavLink } from "react-router-dom";
import { useAuthContext } from '../pages/Auth/hooks/useAuthContext';
import "./Navbar.css";

function Navbar() {
  const { dispatch, decodeToken, token } = useAuthContext(); 
  const decodedToken = decodeToken(token); 
  const role = decodedToken ? decodedToken.role : null; 
  const handleLogout = () => {
    dispatch({ type: 'LOGOUT' });
  };
 let navLinks;
 
  switch (role) {
    case 'Admin':
      navLinks = (
        <>
    {/* <NavLink to="/AddVaccinationCenter">AddVaccinationCenter</NavLink> */}
      {/* <NavLink to="/AddVaccine/:id">AddVaccine</NavLink> */}
      {/* <NavLink to="/CenterDetails/:id">CenterDetails</NavLink> */}
      <NavLink to="/ManageCenters">ManageCenters</NavLink>
     
      <NavLink to="/ManageUsers">ManageUsers</NavLink>
      {/* <NavLink to="/UpdateCenter/:id">UpdateCenter</NavLink> */}
      {/* <NavLink to="/UpdateVaccine/:id">UpdateVaccine</NavLink>
      <NavLink to="/VaccinesDetails/:id">VaccinesDetails</NavLink> */}
        </>
      );
      break;
    case 'VaccinationCenter':
      navLinks = (
        <>
       <NavLink to="/CenterVaccines">CenterVaccines</NavLink>
      <NavLink to="/PatientList">PatientList</NavLink>
      <NavLink to="/ManageCertificate">ManageCertificate</NavLink>


        </>
      );
      break;
      case 'User':
        navLinks = (
          <>
  <NavLink to="/ListCenters">ListCenters</NavLink>
        {/* <NavLink to="/PatientCenterDetails/:id">PatientCenterDetails</NavLink>
        <NavLink to="/PatientVaccineDetails/:id">PatientVaccineDetails</NavLink> */}
        <NavLink to="/ViewCertificate">ViewCertificate</NavLink>
  
  
          </>
        );
        break;
    default:
      navLinks = null;
  }

  return (
    <nav className="nav">
      {navLinks}
    

  
      {!token ? (
        <>
          <NavLink to="/ListCenters">ListCenters</NavLink>
        {/* <NavLink to="/PatientCenterDetails/:id">PatientCenterDetails</NavLink>
        <NavLink to="/PatientVaccineDetails/:id">PatientVaccineDetails</NavLink> */}
           <NavLink to="/ViewCertificate">ViewCertificate</NavLink>
            <NavLink to="/SignUp" className="nav-link text-decoration-none">Sign up</NavLink>
            <NavLink to="/login" className="nav-link text-decoration-none">Login</NavLink>
        </>
      ) : (
        
          <NavLink onClick={handleLogout} to="/login" className="nav-link text-decoration-none">Logout</NavLink>
      )}
    </nav>
  );
}

export default Navbar;
 
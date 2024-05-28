import React, { useContext } from 'react';
import { AuthContext } from './AuthContext';

function NotAuthorized() {
  const { token, decodeToken } = useContext(AuthContext);
  const userRole = decodeToken(token) ? decodeToken(token).role : null;
console.log(userRole)
console.log(token)
  return (
    <div style={{ textAlign: 'center', marginTop: '250px' }}>
      <h1>"You aren't authorized to be here, How did you get here? 🤔"</h1>
      {userRole && <p>Your role: {userRole}</p>}
    </div>
  );
}

export default NotAuthorized;

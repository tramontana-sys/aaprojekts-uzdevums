import React from 'react';

const Header: React.FC = () => {
  return (
    <header>
      <img src="/logo.png" alt="Logo" className='logo' />
      <h1>User Management</h1>
    </header>
  );
};

export default Header;
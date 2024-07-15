import React from 'react';
import packageJson from '../../package.json';

const Footer: React.FC = () => {
  const { version } = packageJson;

  return (
    <footer>
      <p>Version: {version}</p>
    </footer>
  );
};

export default Footer;
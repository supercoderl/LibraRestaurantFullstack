import React, { useContext } from 'react';
import { ThemeContext } from 'src/theme/theme-provider';
import { Container, SwitchIcon, SwitchInput, SwitchPolyline } from './style';

export default function DarkThemeToggler() {
  const themeContext = useContext(ThemeContext);
  return (
    <Container className='group'>
      <SwitchInput onClick={themeContext.toggleTheme} type="checkbox" role="switch" className='peer' />
      <SwitchIcon
        $isdark={themeContext.theme !== "dark"}
        viewBox="0 0 12 12"
        width="12px"
        height="12px"
        aria-hidden="true"
        style={{ right: '0.375em', opacity: themeContext.theme !== "dark" ? 1 : 0 }}
      >
        <g fill="none" stroke="#D14009" strokeWidth="1" strokeLinecap="round">
          <circle cx="6" cy="6" r="2" />
          <g strokeDasharray="1.5 1.5">
            <SwitchPolyline $isdark={themeContext.theme !== "dark"} points="6 10,6 11.5" transform="rotate(0,6,6)" />
            <SwitchPolyline $isdark={themeContext.theme !== "dark"} points="6 10,6 11.5" transform="rotate(45,6,6)" />
            <SwitchPolyline $isdark={themeContext.theme !== "dark"} points="6 10,6 11.5" transform="rotate(90,6,6)" />
            <SwitchPolyline $isdark={themeContext.theme !== "dark"} points="6 10,6 11.5" transform="rotate(135,6,6)" />
            <SwitchPolyline $isdark={themeContext.theme !== "dark"} points="6 10,6 11.5" transform="rotate(180,6,6)" />
            <SwitchPolyline $isdark={themeContext.theme !== "dark"} points="6 10,6 11.5" transform="rotate(225,6,6)" />
            <SwitchPolyline $isdark={themeContext.theme !== "dark"} points="6 10,6 11.5" transform="rotate(270,6,6)" />
            <SwitchPolyline $isdark={themeContext.theme !== "dark"} points="6 10,6 11.5" transform="rotate(315,6,6)" />
          </g>
        </g>
      </SwitchIcon>
      <SwitchIcon
        $isdark={themeContext.theme === "dark"}
        viewBox="0 0 12 12"
        width="12px"
        height="12px"
        aria-hidden="true"
        style={{ left: '0.375em', opacity: themeContext.theme === "dark" ? 1 : 0 }}
      >
        <g fill="none" stroke="#F6F1D5" strokeWidth="1" strokeLinejoin="round" transform="rotate(-45,6,6)">
          <path d="m9,10c-2.209,0-4-1.791-4-4s1.791-4,4-4c.304,0,.598.041.883.105-.995-.992-2.367-1.605-3.883-1.605C2.962.5.5,2.962.5,6s2.462,5.5,5.5,5.5c1.516,0,2.888-.613,3.883-1.605-.285.064-.578.105-.883.105Z" />
        </g>
      </SwitchIcon>
    </Container>
  );
}

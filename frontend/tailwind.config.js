const opacityManager = ({ opacityVariable, opacityValue }, cssVar) => {
  if (opacityValue !== undefined) {
    return `rgba(${cssVar}, ${opacityValue})`;
  }
  if (opacityVariable !== undefined) {
    return `rgba(${cssVar}, var(${opacityVariable}, 1))`;
  }
  return `rgb(${cssVar})`;
};

module.exports = {
  purge: ['./pages/**/*.{js,ts,jsx,tsx}', './components/**/*.{js,ts,jsx,tsx}'],
  darkMode: 'class', // or 'media' or 'class'
  important: true,
  content: [
    './app/**/*.{js,ts,jsx,tsx,mdx}',
    '// Note the addition of the app directory.\r\n    ./pages/**/*.{js,ts,jsx,tsx,mdx}',
    './components/**/*.{js,ts,jsx,tsx,mdx}',
    '// Or if using src directory:\r\n    ./src/**/*.{js,ts,jsx,tsx,mdx}',
  ],
  theme: {
    extend: {
      backgroundColor: {
        primary: 'var(--bg-primary)',
        secondary: 'var(--bg-secondary)',
        tertiary: 'var(--bg-tertiary)',
        productQuantity: '#D12121',
        main: '#D12121',
      },
      dropShadow: {
        coupon: '0 3px 5px rgba(0, 0, 0, 0.5)', // Tùy chỉnh drop-shadow
      },
      backgroundImage: {
        catagoryBackground:
          'url("https://modinatheme.com/foodking/wp-content/uploads/2024/03/catagory-card-shape.png")',
        heroBackground:
          'url("https://wallpapers.com/images/hd/restaurant-background-npda8c1hqh6d0xso.jpg")',
        customRadialBefore: 'radial-gradient(circle at -5% 50%, transparent 25px, gold 26px)',
        customRadialAfter: 'radial-gradient(circle at 105% 50%, transparent 25px, gold 26px)',
        customRadialRight: 'radial-gradient(circle at 110% 50%, transparent 25px, #fff 26px)',
      },
      borderColor: {
        primary: 'var(--bg-primary)',
      },
      boxShadow: {
        readmore: 'inset 0 0 1.6em -0.6em #D12121',
        readmoreIcon: '0.1em 0.1em 0.6em 0.2em #D12121',
        order:
          '0.6px 5.3px 6.6px rgba(0, 0, 0, 0.025), 5px 42px 53px rgba(0, 0, 0, 0.05)',
        buttonCheckout: '5px 5px 10px rgba(0, 0, 0, 0.116)',
        secondaryCategory: 'rgba(0, 0, 0, 0.1) 0px 10px 15px -3px, rgba(0, 0, 0, 0.05) 0px 4px 6px -2px',
        modalFocusInput: '1px 2px 0px 0px #D12121',
        modalInput: '3px 4px 0px 1px #D12121',
        moon: `1.8em 1.5em 0 0em #d9fbff inset, rgba(255, 255, 255, 0.1) 0em -4.2em 0 -2.7em, rgba(255, 255, 255, 0.1) 1.8em 4.2em 0 -2.7em, rgba(255, 255, 255, 0.1) 1.2em 7.8em 0 -2.4em, rgba(255, 255, 255, 0.1) 3.6em 1.2em 0 -2.46em, rgba(255, 255, 255, 0.1) 4.8em 4.8em 0 -2.7em, rgba(255, 255, 255, 0.1) 3.6em 7.8em 0 -2.7em, rgba(255, 255, 255, 0.1) -2.4em 4.2em 0 -2.7em, rgba(255, 255, 255, 0.1) -0.6em 6em 0 -2.7em`,
        sun: '0.45em 0.45em 0 0.75em #fff inset, 0 -0.75em 0 -0.405em #fff, 0.525em -0.525em 0 -0.45em #fff, 0.75em 0 0 -0.405em #fff, 0.525em 0.525em 0 -0.45em #fff, 0 0.75em 0 -0.405em #fff, -0.525em 0.525em 0 -0.45em #fff, -0.75em 0 0 -0.405em #fff, -0.525em -0.525em 0 -0.45em #fff',
        switchInput: '0 0 0 0.125em hsla(223,90%,50%,0), 0.125em 0.125em 0.25em hsla(223,90%,10%,0.2)',
        focusVisible: '0 0 0 0.125em hsl(223,90%,50%), 0.125em 0.125em 0.25em hsla(223,90%,10%,0.2)',
      },
      textColor: {
        primary: 'var(--text-primary)',
        secondary: 'var(--text-secondary)',
        productQuantity: '#D12121',
      },
      colors: {
        purple: props => {
          return opacityManager(props, 'var(--color-purple)');
        },
        orange: props => {
          return opacityManager(props, 'var(--color-orange)');
        },
        yellow: props => {
          return opacityManager(props, 'var(--color-yellow)');
        },
        main: '#D12121',
        background: '#F4F1EA',
        darkBlue: 'hsl(223, 47%, 23%)',
        veryPaleBlue: 'hsl(225, 100%, 98%)',
        desaturatedBlue: 'hsl(224, 23%, 55%)',
      },
      maxWidth: {
        cart: '440px',
      },
      width: {
        readmoreIcon: 'calc(100% - 0.6em)',
      },
      translate: {
        readmoreSvg: '0.1em',
        modalFocusInput: '4px'
      },
      rotate: {
        250: '250deg',
        360: '360deg',
      },
      transitionProperty: {
        'opacity-transform': 'opacity, transform',
      },
      transitionDuration: {
        '500': '500ms',
      },
      transitionTimingFunction: {
        'custom-bezier-out': 'cubic-bezier(0.05, 0.76, 0.06, 0.86)',
        'custom-bezier-in': 'cubic-bezier(0.76,0.05,0.86,0.06)'
      },
      keyframes: {
        fadeIn: {
          '0%': { opacity: '0' },
          '100%': { opacity: '1' },
        },
        slideIn: {
          '0%': { transform: 'translateX(-100%)', opacity: '0' },
          '100%': { transform: 'translateX(0)', opacity: '1' },
        },
        slideOut: {
          '0%': { transform: 'translateX(0)', opacity: '1' },
          '100%': { transform: 'translateX(100%)', opacity: '0' },
        },
        move: {
          '0%': { transform: 'translateY(0)' },
          '25%': { transform: 'translateY(-25px)' },
          '50%': { transform: 'translateY(-15px)' },
          '75%': { transform: 'translateY(-25px)' },
          '100%': { transform: 'translateY(0)' },
        },
        rotate360: {
          '0%': { transform: 'rotate(0deg)' },
          '100%': { transform: 'rotate(360deg)' }
        },
        fadeInUp: {
          '0%': { opacity: 0, transform: 'translateY(20px)' },
          '100%': { opacity: 1, transform: 'translateY(0px)' }
        }
      },
      animation: {
        fadeIn: 'fadeIn 500ms ease-in-out',
        slideIn: 'slideIn 500ms ease-in-out',
        slideOut: 'slideOut 500ms ease-in-out',
        move: 'move 7s infinite',
        rotate360: 'rotate360 8s linear infinite',
        spinSlow: 'spin 3s linear infinite',
        fadeInUp: 'fadeInUp 0.5s ease'
      },
      fontFamily: {
        barcode: ['"Libre Barcode 128 Text"', 'cursive'], // Thêm font tùy chỉnh
        lobster: ["Lobster", "cursive"],
      },
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
};

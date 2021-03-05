import '../styles/App.sass'
import '../styles/mainStyles.css'

export default function App({ Component, pageProps }) {
  return (
      <div>
        <Component {...pageProps} />
      </div>
  )
}


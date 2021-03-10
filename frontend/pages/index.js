import React, { useState } from 'react'

export default function Index() {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    const onSubmit = () => {
        const payload = {
            email: email,
            password: password
        }
        fetch('https://localhost:5001/api/users/authenticate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin' : '*'
            },
            body: JSON.stringify(payload)
        })
            .then(data => console.log(data))
            .catch(error => alert(error))

    }
  return (
    <div className={'columns is-vcentered is-centered'}>
        <div className="field">
            <label className="label" style={{ color: 'white' }}>Email</label>
            <div className="control has-icons-left has-icons-right">
                <input
                    className="input is-success"
                    type="email" placeholder="Correo@mail.com"
                    onChange={e => setEmail(e.target.value)}/>
                <span className="icon is-small is-left">
                    <i className="fas fa-user"/>
                </span>
                <span className="icon is-small is-right">
                    <i className="fas fa-check"/>
                </span>
            </div>
        </div>

        <div className="field">
            <label className="label" style={{ color: 'white' }}>Password</label>
            <div className="control has-icons-left has-icons-right">
                <input
                    className="input is-danger"
                    type="password"
                    placeholder="Password"
                    onChange={e => setPassword(e.target.value)}/>
                <span className="icon is-small is-left">
                    <i className="fas fa-envelope"/>
                </span>
                <span className="icon is-small is-right">
                    <i className="fas fa-exclamation-triangle"/>
                </span>
            </div>
        </div>


        <div className="field is-grouped" style={{ margin: '2rem 0 0 2rem' }}>
            <div className="control">
                <button className="button is-link" onClick={onSubmit}>Entrar</button>
            </div>
        </div>
    </div>
  )
}

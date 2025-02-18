import { createClient } from '@/prismicio';
import NavBar from './NavBar';
import React from 'react'



export default async function Header () {

    const client = createClient();
    const settings = await client.getSingle('settings');
    


  return (
    <header className='bg-black'>
    <NavBar settings={settings}/>
    </header>
  )
}
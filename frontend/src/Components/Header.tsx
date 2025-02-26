import { createClient } from '@/prismicio';
import NavBar from './NavBar';
import React from 'react'
import StickyNav from './ui/StickyNav';



export default async function Header () {

    const client = createClient();
    const settings = await client.getSingle('settings');
    


  return (
    <header>
    <StickyNav settings={settings}/>
    </header>
  )
}
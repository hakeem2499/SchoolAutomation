import exp from 'constants'
import React, { JSX } from 'react'

type Props = {
    onClick?: (e: React.MouseEvent<HTMLButtonElement>) => void
    text: string
}

const Button:React.FC<Props> = ({onClick, text}):JSX.Element => {
  return (
    <button  className='px-4 md:px-6 py-4 bg-brand hover:bg-brandWhite text-brandWhite hover:text-black transition-colors duration-200 rounded-md' onClick={onClick}>{text}</button>
  )
}

export default Button;
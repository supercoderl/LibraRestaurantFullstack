import { GithubOutlined } from '@ant-design/icons'
import React from 'react'

export default function DashboardFooter() {
  return (
    <div className="footer">
      <GithubOutlined />
      <a href='https://github.com/supercoderl/Libra-Restaurant-Frontend' target='_blank' style={{ margin: 5 }}>Github</a>
    </div>
  )
}
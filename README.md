# Network Packet Sniffer

A simple network packet sniffer application developed in C# using WinPcap. The tool allows you to capture and analyze network packets in real-time, including details such as source and destination IP addresses, ports, and protocol types (e.g., TCP).

## Prerequisites

Before running the application, make sure you have the following:

- **.NET Framework**: The application is built using C# with .NET Framework. Ensure you have the appropriate version of the framework installed.
- **Npcap**: You need to install the Npcap library, which is used for network packet capturing.

  - alternativly, WinPcap from [WinPcap official website](https://www.winpcap.org/) and install it.
  - Download Npcap from  [Npcap official website](https://nmap.org/npcap/).

## Installing Dependencies

1. Install **WinPcap** or **Npcap** as a prerequisite.
2. Ensure that the **PacketDotNet** and **SharpPcap** libraries are added to the project. You can install these via NuGet:

   ```bash
   Install-Package PacketDotNet
   Install-Package SharpPcap

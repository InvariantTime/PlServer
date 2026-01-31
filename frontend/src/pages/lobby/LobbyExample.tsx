import React, { useState, useEffect } from 'react';
import { Plus, Users, Clock, Zap, MoreVertical, Trash2, Gamepad2, MessageCircle } from 'lucide-react';

// Типы данных
interface Session {
  id: string;
  title: string;
  type: 'game' | 'chat' | 'meeting';
  participants: number;
  maxParticipants: number;
  createdAt: Date;
  status: 'active' | 'waiting' | 'locked';
  host: string;
}

// Генератор ID
const generateId = () => Math.random().toString(36).substr(2, 9);

// Начальные данные
const initialSessions: Session[] = [
  {
    id: '1',
    title: 'Cyberpunk 2077 Night City Run',
    type: 'game',
    participants: 3,
    maxParticipants: 4,
    createdAt: new Date(Date.now() - 1000 * 60 * 30),
    status: 'active',
    host: 'Neo'
  },
  {
    id: '2',
    title: 'Strategy Planning Alpha',
    type: 'meeting',
    participants: 2,
    maxParticipants: 8,
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 2),
    status: 'waiting',
    host: 'Trinity'
  },
  {
    id: '3',
    title: 'Midnight Hackers Chat',
    type: 'chat',
    participants: 12,
    maxParticipants: 50,
    createdAt: new Date(Date.now() - 1000 * 60 * 60 * 24),
    status: 'active',
    host: 'Morpheus'
  }
];

export const LobbyPage = () => {
  const [sessions, setSessions] = useState<Session[]>(initialSessions);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [newSessionTitle, setNewSessionTitle] = useState('');
  const [newSessionType, setNewSessionType] = useState<Session['type']>('game');
  const [hoveredSession, setHoveredSession] = useState<string | null>(null);

  // Форматирование времени
  const formatTime = (date: Date) => {
    const hours = Math.floor((Date.now() - date.getTime()) / (1000 * 60 * 60));
    if (hours < 1) return 'Just now';
    if (hours === 1) return '1 hour ago';
    return `${hours} hours ago`;
  };

  // Создание сессии
  const handleCreateSession = (e: React.FormEvent) => {
    e.preventDefault();
    if (!newSessionTitle.trim()) return;

    const newSession: Session = {
      id: generateId(),
      title: newSessionTitle,
      type: newSessionType,
      participants: 1,
      maxParticipants: newSessionType === 'chat' ? 50 : 4,
      createdAt: new Date(),
      status: 'waiting',
      host: 'You'
    };

    setSessions([newSession, ...sessions]);
    setNewSessionTitle('');
    setIsModalOpen(false);
  };

  // Удаление сессии
  const handleDeleteSession = (id: string, e: React.MouseEvent) => {
    e.stopPropagation();
    setSessions(sessions.filter(s => s.id !== id));
  };

  // Получение иконки типа
  const getTypeIcon = (type: Session['type']) => {
    switch (type) {
      case 'game': return <Gamepad2 className="w-4 h-4" />;
      case 'chat': return <MessageCircle className="w-4 h-4" />;
      case 'meeting': return <Zap className="w-4 h-4" />;
    }
  };

  // Получение цвета статуса
  const getStatusColor = (status: Session['status']) => {
    switch (status) {
      case 'active': return 'bg-emerald-500 shadow-[0_0_10px_rgba(16,185,129,0.5)]';
      case 'waiting': return 'bg-amber-500 shadow-[0_0_10px_rgba(245,158,11,0.5)]';
      case 'locked': return 'bg-rose-500 shadow-[0_0_10px_rgba(244,63,94,0.5)]';
    }
  };

  return (
    <div className="min-h-screen bg-slate-950 text-slate-200 font-mono selection:bg-cyan-500/30">
      {/* Фоновые эффекты */}
      <div className="fixed inset-0 overflow-hidden pointer-events-none">
        <div className="absolute top-0 left-1/4 w-96 h-96 bg-cyan-500/10 rounded-full blur-3xl animate-pulse" />
        <div className="absolute bottom-0 right-1/4 w-96 h-96 bg-purple-500/10 rounded-full blur-3xl animate-pulse delay-1000" />
        <div className="absolute inset-0 bg-[url('data:image/svg+xml,%3Csvg%20width%3D%2260%22%20height%3D%2260%22%20viewBox%3D%220%200%2060%2060%22%20xmlns%3D%22http%3A//www.w3.org/2000/svg%22%3E%3Cg%20fill%3D%22none%22%20fill-rule%3D%22evenodd%22%3E%3Cg%20fill%3D%22%239C92AC%22%20fill-opacity%3D%220.03%22%3E%3Cpath%20d%3D%22M36%2034v-4h-2v4h-4v2h4v4h2v-4h4v-2h-4zm0-30V0h-2v4h-4v2h4v4h2V6h4V4h-4zM6%2034v-4H4v4H0v2h4v4h2v-4h4v-2H6zM6%204V0H4v4H0v2h4v4h2V6h4V4H6z%22/%3E%3C/g%3E%3C/g%3E%3C/svg%3E')] opacity-20" />
      </div>

      <div className="relative max-w-4xl mx-auto px-6 py-12">
        {/* Шапка */}
        <header className="mb-12 space-y-2">
          <div className="flex items-center gap-3 mb-4">
            <div className="w-12 h-12 bg-gradient-to-br from-cyan-400 to-blue-600 rounded-xl flex items-center justify-center shadow-lg shadow-cyan-500/20">
              <Zap className="w-6 h-6 text-white" />
            </div>
            <div>
              <h1 className="text-4xl font-bold bg-gradient-to-r from-cyan-400 via-blue-400 to-purple-400 bg-clip-text text-transparent tracking-tight">
                NEXUS LOBBY
              </h1>
              <p className="text-slate-500 text-sm uppercase tracking-widest">
                {sessions.length} Active Sessions // Connection: Stable
              </p>
            </div>
          </div>
        </header>

        {/* Список сессий */}
        <div className="space-y-4 mb-8">
          {sessions.map((session, index) => (
            <div
              key={session.id}
              onMouseEnter={() => setHoveredSession(session.id)}
              onMouseLeave={() => setHoveredSession(null)}
              className={`
                group relative p-6 rounded-2xl border backdrop-blur-md
                transition-all duration-500 ease-out cursor-pointer
                ${hoveredSession === session.id 
                  ? 'bg-slate-800/50 border-cyan-500/50 shadow-[0_0_30px_rgba(6,182,212,0.15)] translate-x-2' 
                  : 'bg-slate-900/40 border-slate-800 hover:border-slate-700'
                }
              `}
              style={{
                animationDelay: `${index * 100}ms`,
                animation: 'slideIn 0.5s ease-out forwards'
              }}
            >
              {/* Градиентная линия слева */}
              <div className={`absolute left-0 top-0 bottom-0 w-1 rounded-l-2xl transition-all duration-300 ${
                session.type === 'game' ? 'bg-gradient-to-b from-cyan-400 to-blue-600' :
                session.type === 'chat' ? 'bg-gradient-to-b from-purple-400 to-pink-600' :
                'bg-gradient-to-b from-amber-400 to-orange-600'
              } ${hoveredSession === session.id ? 'opacity-100' : 'opacity-50'}`} />

              <div className="flex items-center justify-between pl-4">
                <div className="flex-1">
                  <div className="flex items-center gap-3 mb-2">
                    <span className={`
                      px-2 py-1 rounded text-xs font-bold uppercase tracking-wider flex items-center gap-1.5
                      ${session.type === 'game' ? 'bg-cyan-500/10 text-cyan-400 border border-cyan-500/20' :
                        session.type === 'chat' ? 'bg-purple-500/10 text-purple-400 border border-purple-500/20' :
                        'bg-amber-500/10 text-amber-400 border border-amber-500/20'}
                    `}>
                      {getTypeIcon(session.type)}
                      {session.type}
                    </span>
                    <div className={`w-2 h-2 rounded-full ${getStatusColor(session.status)}`} />
                    <span className="text-xs text-slate-500 uppercase tracking-wider">
                      {session.status}
                    </span>
                  </div>
                  
                  <h3 className="text-xl font-bold text-slate-200 mb-1 group-hover:text-cyan-400 transition-colors">
                    {session.title}
                  </h3>
                  
                  <div className="flex items-center gap-4 text-sm text-slate-500">
                    <span className="flex items-center gap-1.5">
                      <Users className="w-4 h-4" />
                      {session.participants}/{session.maxParticipants}
                    </span>
                    <span className="flex items-center gap-1.5">
                      <Clock className="w-4 h-4" />
                      {formatTime(session.createdAt)}
                    </span>
                    <span className="text-slate-600">// Host: {session.host}</span>
                  </div>
                </div>

                <div className="flex items-center gap-2 opacity-0 group-hover:opacity-100 transition-opacity">
                  <button 
                    onClick={(e) => handleDeleteSession(session.id, e)}
                    className="p-2 hover:bg-rose-500/10 hover:text-rose-400 rounded-lg transition-colors"
                    title="Delete session"
                  >
                    <Trash2 className="w-5 h-5" />
                  </button>
                  <button className="px-4 py-2 bg-slate-800 hover:bg-cyan-500 hover:text-slate-900 text-cyan-400 rounded-lg text-sm font-bold transition-all border border-cyan-500/30 hover:border-cyan-500">
                    JOIN
                  </button>
                </div>
              </div>
            </div>
          ))}

          {sessions.length === 0 && (
            <div className="text-center py-20 text-slate-600 border-2 border-dashed border-slate-800 rounded-2xl">
              <p className="text-lg">No active sessions found</p>
              <p className="text-sm mt-2">Create a new one to get started</p>
            </div>
          )}
        </div>

        {/* Кнопка добавления */}
        <div className="flex justify-center pt-4">
          <button
            onClick={() => setIsModalOpen(true)}
            className="group relative w-full max-w-md p-4 rounded-2xl border-2 border-dashed border-slate-700 hover:border-cyan-500/50 bg-slate-900/20 hover:bg-slate-800/30 transition-all duration-300"
          >
            <div className="flex items-center justify-center gap-3 text-slate-500 group-hover:text-cyan-400 transition-colors">
              <div className="w-10 h-10 rounded-full bg-slate-800 group-hover:bg-cyan-500/20 flex items-center justify-center transition-all group-hover:scale-110 group-hover:rotate-90">
                <Plus className="w-6 h-6" />
              </div>
              <span className="text-lg font-bold uppercase tracking-wider">Create New Session</span>
            </div>
            <div className="absolute inset-0 rounded-2xl bg-gradient-to-r from-cyan-500/0 via-cyan-500/5 to-purple-500/0 opacity-0 group-hover:opacity-100 transition-opacity" />
          </button>
        </div>
      </div>

      {/* Модальное окно */}
      {isModalOpen && (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-slate-950/80 backdrop-blur-sm">
          <div 
            className="absolute inset-0" 
            onClick={() => setIsModalOpen(false)}
          />
          
          <div className="relative w-full max-w-md bg-slate-900 border border-slate-700 rounded-3xl shadow-2xl shadow-cyan-500/10 p-8 transform scale-100 animate-in fade-in zoom-in duration-200">
            {/* Декоративные углы */}
            <div className="absolute top-0 left-0 w-8 h-8 border-t-2 border-l-2 border-cyan-500 rounded-tl-3xl" />
            <div className="absolute top-0 right-0 w-8 h-8 border-t-2 border-r-2 border-cyan-500 rounded-tr-3xl" />
            <div className="absolute bottom-0 left-0 w-8 h-8 border-b-2 border-l-2 border-cyan-500 rounded-bl-3xl" />
            <div className="absolute bottom-0 right-0 w-8 h-8 border-b-2 border-r-2 border-cyan-500 rounded-br-3xl" />

            <h2 className="text-2xl font-bold text-center mb-2 bg-gradient-to-r from-cyan-400 to-purple-400 bg-clip-text text-transparent">
              Initialize New Session
            </h2>
            <p className="text-slate-500 text-center text-sm mb-8 uppercase tracking-widest">
              Configure connection parameters
            </p>

            <form onSubmit={handleCreateSession} className="space-y-6">
              <div className="space-y-2">
                <label className="text-xs font-bold text-slate-400 uppercase tracking-wider ml-1">
                  Session Title
                </label>
                <input
                  type="text"
                  value={newSessionTitle}
                  onChange={(e) => setNewSessionTitle(e.target.value)}
                  placeholder="Enter session name..."
                  className="w-full px-4 py-3 bg-slate-950 border border-slate-700 rounded-xl text-slate-200 placeholder-slate-600 focus:outline-none focus:border-cyan-500 focus:ring-2 focus:ring-cyan-500/20 transition-all"
                  autoFocus
                />
              </div>

              <div className="space-y-2">
                <label className="text-xs font-bold text-slate-400 uppercase tracking-wider ml-1">
                  Session Type
                </label>
                <div className="grid grid-cols-3 gap-3">
                  {(['game', 'chat', 'meeting'] as const).map((type) => (
                    <button
                      key={type}
                      type="button"
                      onClick={() => setNewSessionType(type)}
                      className={`
                        px-4 py-3 rounded-xl border text-sm font-bold uppercase transition-all
                        ${newSessionType === type
                          ? 'bg-cyan-500/10 border-cyan-500 text-cyan-400 shadow-[0_0_20px_rgba(6,182,212,0.2)]'
                          : 'bg-slate-950 border-slate-700 text-slate-500 hover:border-slate-600'
                        }
                      `}
                    >
                      {type}
                    </button>
                  ))}
                </div>
              </div>

              <div className="flex gap-3 pt-4">
                <button
                  type="button"
                  onClick={() => setIsModalOpen(false)}
                  className="flex-1 px-6 py-3 rounded-xl border border-slate-700 text-slate-400 font-bold hover:bg-slate-800 transition-colors uppercase tracking-wider text-sm"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  disabled={!newSessionTitle.trim()}
                  className="flex-1 px-6 py-3 rounded-xl bg-gradient-to-r from-cyan-500 to-blue-600 text-white font-bold hover:from-cyan-400 hover:to-blue-500 disabled:opacity-50 disabled:cursor-not-allowed transition-all shadow-lg shadow-cyan-500/25 uppercase tracking-wider text-sm"
                >
                  Create
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      <style>{`
        @keyframes slideIn {
          from {
            opacity: 0;
            transform: translateY(20px);
          }
          to {
            opacity: 1;
            transform: translateY(0);
          }
        }
      `}</style>
    </div>
  );
}